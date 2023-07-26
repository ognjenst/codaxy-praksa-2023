import { LabeledContainer } from "cx/widgets";

export const HumidityComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Humidity",
                className: "text-base text-slate-600",
            }}
        >
            <div text-tpl="{$page.device.humidity.value} %" className="flex-1 text-slate-600 ml-4" />
        </LabeledContainer>
    </cx>
);
