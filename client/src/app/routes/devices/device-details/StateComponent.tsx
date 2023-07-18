import { Switch } from "cx/widgets";

export const StateComponent = () => (
    <cx>
        <Switch
            label={{
                text: "State",
                className: "text-xl",
            }}
            on-bind="$page.device.state"
            text-expr="{$page.device.state} ? 'ON' : 'OFF'"
        />
    </cx>
);
