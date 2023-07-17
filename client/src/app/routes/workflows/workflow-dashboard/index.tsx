import { Icon, Switch } from "cx/widgets";

export default () => (
    <cx>
        <div className="relative">
            <span className="relative -top-4 left-5 bg-white p-2">Morning Routine</span>
            <Icon className="absolute top-2 right-2 h-10 w-10" name="calendar"/>
        </div> 

        <div className="p-10">
            <div className="flex flex-col">
                <div className="bg-red-400 flex-1"><Switch label="Enabled" off-bind="$page.check" readOnly/></div>
                <div className="bg-blue-400 flex-1"><Switch label="Status" off-bind="$page.check" readOnly/></div>
            </div>
        </div>
    </cx>
);