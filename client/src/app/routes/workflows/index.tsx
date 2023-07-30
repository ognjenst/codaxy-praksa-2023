import { Button } from "cx/widgets";
import Controller from "./Controller";
import WorkflowDashboard from "./workflow-dashboard";
import WorkflowsList from "./workflows-list";
import WorkflowsUndoneList from "./workflows-undone-list";
import { CodeMirror } from "../../components/CodeMirror";

export default () => (
    <cx>
        <div className="m-4 flex flex-col mt-6" controller={Controller}>
            <div class="grid grid-cols-1 md:grid-cols-5 lg:grid-cols-5 gap-3 gap-x-0 md:gap-x-3 lg-gap-3">
                <div>
                    <Button text="Insert" className="w-full mb-2 text-gray-600" onClick="openWindow" />
                    <div text="Created workflows:" className="pb-2 pt-2 text-gray-600" />
                    <WorkflowsList />
                    <div text="Undone workflows:" className="pb-2 pt-2 text-gray-600" />
                    <WorkflowsUndoneList />
                </div>

                <div class="mt-3 md:mt-0 lg:mt-0 bg-white border border-gray-200 col-span-4 rounded-sm">
                    <WorkflowDashboard  />
                </div>
            </div>
        </div>
    </cx>
);
