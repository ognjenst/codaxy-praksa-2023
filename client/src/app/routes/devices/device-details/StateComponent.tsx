import { Switch } from "cx/widgets";

export const StateComponent = () => (
    <cx>
        <div className="flex gap-16">
            <div className="text-xl">State</div>
            <Switch on-bind="$page.device.state" text-expr="{$page.device.state} ? 'ON' : 'OFF'" />
            <hr />
        </div>
    </cx>
);
