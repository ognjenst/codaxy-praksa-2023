import { List } from "cx/widgets";
import Controller from './Controller';

export default () => (
    <cx>
        <div controller={Controller} className="bg-blue-200 border-2">
            <List records-bind="$page.workflows">
                <p text-tpl="{$record.name}" />
            </List>
        </div>       
    </cx>
);