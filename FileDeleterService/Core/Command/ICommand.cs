using System;
using System.Collections.Generic;
using System.Text;

namespace FileDeleterService.Core.Command
{
    public interface ICommand
    {
        void Execute();
        string GetName();
    }
}
