using System;

namespace LockNotch.Services;

public interface IBrightnessService
{
    event EventHandler<int>? BrightnessChanged;
    void Start();
}
