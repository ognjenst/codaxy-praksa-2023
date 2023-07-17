import WorkflowDashboard from "./workflow-dashboard";

export default () => (
    <cx>
        <div className="m-4 flex flex-col">
            <div class="grid grid-cols-5 gap-3">
                <div class="bg-blue-100">1st col</div>
                <div class="bg-white border-2 border-gray-700 col-span-4 rounded-sm">
                    <WorkflowDashboard />
                </div>
            </div>
        </div>
    </cx>
);