import { List } from "cx/widgets";
import Controller from "./Controller";
import { KeySelection } from "cx/ui";

export default () => (
    <cx>
        <div controller={Controller} className="border border-gray-200">
            <List
                records-bind="$page.undoneWorkflows"
                emptyText="No undone workflows"
                mod="bordered"
                onItemClick={(e, { controller, store }) => {
                    controller.invokeMethod("itemClicked", store.get("$record"));
                }}
                selection={{
                    type: KeySelection,
                    keyField: "name",
                    bind: "$page.workflowSelection",
                }}
            >
                <p text-bind="$record.name" />
            </List>
        </div>
    </cx>
);
