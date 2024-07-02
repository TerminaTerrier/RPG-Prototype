using Godot;
using System;

public interface IInspectable
{
	void Inspect();
	void OutputInspectionResult();
	bool GetInspectionStatus();
	void EndInspection();
}
