using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MinerState {

}

public class MineCommand : ICommand {
  private Ship _ship;
  private CmpMining _cmpMining;
  private CmpCargo _cmpCargo;
  private uint _minedPerTick;
  private IMineable _target;
  private uint _miningProgress;
  private ResourceData _resourceData;
  private bool _isDone = false;

  public MineCommand(Ship ship, IMineable target) {
    this._ship = ship;
    this._target = target;
    this._resourceData = ResourceManager.Instance.GetResource(this._target.GetResourceType());
    this._cmpMining = ship.CmpMining;
    this._minedPerTick = _cmpMining.MinedPerTick;
    this._cmpCargo = ship.CmpCargo;
    this._miningProgress = 0;
  }

  ~MineCommand() {
    Debug.Log("Mine destructor");
  }

  public void SimUpdate() {
    // Done if ship is full or target rsource is empty
    if (_cmpCargo.IsFull || _target.GetAmountRemaining() == 0) {
      _isDone = true;
      return;
    }

    _miningProgress += _minedPerTick;
    if (_miningProgress > _resourceData.MiningDifficulty) {
      // Run 1 tick on each mining attachment
      // MiningDifficulty
      // Miner speed
      uint potentialAmountMined = _miningProgress / _resourceData.MiningDifficulty;
      if (potentialAmountMined > _cmpCargo.EmptySpace) {
        potentialAmountMined = _cmpCargo.EmptySpace;
      }

      if (potentialAmountMined > 0) {
        _miningProgress = _miningProgress - (potentialAmountMined * _resourceData.MiningDifficulty);
        if (_miningProgress > _resourceData.MiningDifficulty) {
          _miningProgress = _resourceData.MiningDifficulty;
        }

        uint minedAmount = _target.OnMine(potentialAmountMined);

        ResourceAmount ra;
        ra.Amount = minedAmount;
        ra.Type = _resourceData.Type;
        _cmpCargo.LoadCargo(ra);
      }
    }
  }

  public bool IsDone() {
    return _isDone;
  }
}
