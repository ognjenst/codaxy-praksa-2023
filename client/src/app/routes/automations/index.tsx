import { List, LookupField, PureContainer } from "cx/widgets";
import Controller from "./Controller";

export default  () => (
    <cx>
        <PureContainer controller={Controller}>
            <div className="flex">
                <div className="">
                    <List>

                    </List>
                </div>
                <div className="flex-1">
                    <LookupField label ="Trigger type" options={options} text-bind ="$page.options.text"/>
                </div>
            </div>
        </PureContainer>
    </cx>
);

const options = [
    {id: 1, text: 'Periodic Trigger'},
    {id: 2, text: 'IoT Trigger'}
];