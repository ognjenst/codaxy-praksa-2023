import { List } from "cx/widgets";
import Controller from './Controller';

export default () => (
    <cx>
        <div controller={Controller} className="border-2 border-gray-700">
            <List records-bind="$page.workflows" mod='bordered' onItemClick={(e, {store}) => { 
                let currentWorkflow = store.get('$record');
                store.set('$page.currentWorkflow', currentWorkflow);
                 }}>
                <p text-tpl="{$record.name}" />
            </List>
        </div>       
    </cx>
);