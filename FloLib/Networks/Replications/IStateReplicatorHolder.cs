using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Networks.Replications;
public interface IStateReplicatorHolder<S> where S : struct
{
    void OnStateChange(S oldState, S state, bool isRecall);
    public StateReplicator<S> Replicator { get; }
}
