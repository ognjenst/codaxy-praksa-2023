import { Button } from "cx/widgets";
import WorkflowDashboard from "./workflow-dashboard";
import WorkflowsList from "./workflows-list";
import { openInsertUpdateWindow } from "./update-insert-workflow";
import WorkflowsUndoneList from "./workflows-undone-list";
import Controller from "./Controller";

export default () => (
    <cx>
        <div className="m-4 flex flex-col" controller={Controller}>
            <div class="grid grid-cols-3 sm:grid-cols-2 md:grid-cols-5 lg:grid-cols-5 gap-3">
                <div>
                    <Button text="Insert" className="w-full mb-2 text-gray-600" onClick="openWindow" />
                    <div text="Created workflows:" className="pb-2 pt-2 text-gray-600" />
                    <WorkflowsList />
                    <div text="Undone workflows:" className="pb-2 pt-2 text-gray-600" />
                    <WorkflowsUndoneList />
                </div>

                <div class="mt-3 md:mt-0 lg:mt-0 bg-white border border-gray-200 col-span-4 rounded-sm">
                    <WorkflowDashboard />
                </div>
            </div>
        </div>
    </cx>
);
