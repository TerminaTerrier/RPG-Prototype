using Godot;
using System;

public interface IState 
{
   public ITransition FSM {get; set;} 
   void Enter(Variant StateController);
   void Update(Variant StateController, float delta);
   void Exit(Variant StateController);
}
