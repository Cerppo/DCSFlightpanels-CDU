﻿using System.Threading;

namespace NonVisuals.Interfaces
{

    public enum EnumStreamDeckActionType
    {
        Unknown = 0,
        KeyPress = 1,
        DCSBIOS = 2,
        OSCommand = 4,
        LayerNavigation = 16
    }

    public interface IStreamDeckButtonAction
    {
        EnumStreamDeckActionType ActionType { get; }
        void Execute(CancellationToken cancellationToken);
    }
}
