import { LabeledContainer } from "cx/widgets";

export const EnergyComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Power",
                className: "text-base text-slate-600",
            }}
        >
            <div text-tpl="{$page.device.energy.power} kWh" className="flex-1 text-slate-600 ml-4" />
        </LabeledContainer>
    </cx>
);
