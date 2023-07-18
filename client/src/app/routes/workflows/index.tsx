import WorkflowDashboard from "./workflow-dashboard";
import WorkflowsList from "./workflows-list";

export default () => (
    <cx>
        <div className="m-4 flex flex-col">
            <div class="grid grid-cols-2 sm:grid-cols-2 md:grid-cols-5 lg:grid-cols-5 gap-3">
                <div>
                    <WorkflowsList />
                </div>
                <div class="mt-3 md:mt-0 lg:mt-0 bg-white border-2 border-gray-700 col-span-4 rounded-sm">
                    <WorkflowDashboard />
                </div>
            </div>
        </div>
    </cx>
);