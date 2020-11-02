using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand {
  // Runs one tick for this command
  void SimUpdate();
  bool IsDone();
}
