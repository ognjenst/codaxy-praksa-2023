import { LabeledContainer } from "cx/widgets";

export const TemperatureComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Temperature",
                className: "text-base text-slate-600",
            }}
        >
            <div text-tpl="{$page.device.temperature.value} ˚C" className="flex-1 text-slate-600 ml-4" />
        </LabeledContainer>
    </cx>
);
