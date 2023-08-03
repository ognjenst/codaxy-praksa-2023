import { List } from "cx/widgets";
import Controller from "./Controller";
import { KeySelection, PropertySelection } from "cx/ui";

export default () => (
    <cx>
        <div controller={Controller} className="border border-gray-200 overflow-y-auto">
            <List
                records-bind="$page.workflows"
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
