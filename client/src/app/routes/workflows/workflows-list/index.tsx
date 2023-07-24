import { List } from "cx/widgets";
import Controller from "./Controller";

export default () => (
    <cx>
        <div controller={Controller} className="border border-gray-200">
            <List
                records-bind="$page.workflows"
                mod="bordered"
                onItemClick={(e, { controller, store }) => {
                    controller.invokeMethod("itemClicked", store.get("$record"));
                }}
            >
                <p text-bind="$record.name" />
            </List>
        </div>
    </cx>
);
