﻿using SOC.Conductor.Entities;

namespace SOC.Conductor.Contracts
{
    public interface IAutomationRepository : IRepositoryBase<Automation>
    {
        Task<List<Workflow>> GetWorkflowsByTriggerIdAsync(int triggerId);

        Task<Automation> GetById(int triggerId, int workflowId);
    }
}
