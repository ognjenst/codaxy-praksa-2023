import { Switch } from "cx/widgets";

export const StateComponent = () => (
    <cx>
        <Switch
            label={{
                text: "State",
                className: "text-base text-slate-600",
            }}
            value-bind="$page.device.state.state"
            text-expr="{$page.device.state.state} ? 'ON' : 'OFF'"
            className="text-slate-600"
        />
    </cx>
);
