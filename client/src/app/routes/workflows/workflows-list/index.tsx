import { List } from "cx/widgets";
import Controller from "./Controller";

export default () => (
    <cx>
        <div controller={Controller} className="border border-gray-200">
            <List
                records-bind="$page.workflows"
                mod="bordered"
                onItemClick={(e, { store }) => {
                    let currentWorkflow = store.get("$record");
                    store.set("$page.currentWorkflow", currentWorkflow);
                    store.set("$page.arrTasks", store.get("$page.currentWorkflow.tasks"));
                }}
            >
                <p text-tpl="{$record.name}" />
            </List>
        </div>
    </cx>
);
