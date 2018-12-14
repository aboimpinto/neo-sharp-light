﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NeoSharpLight.RPC.NodeManager.RedisDumpDb
{
    public interface IDbAccess
    {
        int GetBlockCount();

        void SaveBlock(int index, string block);

        void SaveBlockCount(int index);
    }
}