using Agents;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Networks.PayloadStructs;
public struct PL_Agent
{
    public ushort AgentID;

    public PL_Agent()
    {
        AgentID = 0;
    }

    public PL_Agent(Agent agent)
    {
        AgentID = agent.GlobalID;
    }

    public void Set(Agent agent)
    {
        AgentID = agent.GlobalID;
    }

    public bool TryGet(out Agent agent)
    {
        if (!SNet_Replication.TryGetReplicator(AgentID, out var replicator))
        {
            agent = null;
            return false;
        }

        if (replicator == null)
        {
            agent = null;
            return false;
        }

        if (replicator.ReplicatorSupplier == null)
        {
            agent = null;
            return false;
        }

        var mb = replicator.ReplicatorSupplier.TryCast<MonoBehaviour>();
        if (mb == null)
        {
            agent = null;
            return false;
        }

        agent = mb.GetComponent<Agent>();
        return agent != null;
    }
}
