using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ship component for all kinds of mining
public class CmpMining : BaseShipComponent {
  class Miner {
    public MiningAttachment Data;
  }

  List<Miner> Miners = new List<Miner>();
  
  public void Add(MiningAttachment attachment) {
    Miner m = new Miner();
    m.Data = attachment;
    Miners.Add(m);
  }
}
