import { LabeledContainer, NumberField, Slider } from "cx/widgets";

export const BrightnessComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Brightness",
                className: "text-base text-slate-600",
            }}
        >
            <Slider
                value-bind="$page.device.light.brightness"
                valueTooltip={{
                    text: { tpl: "{$page.device.light.brightness:n;2}" },
                    placement: "up",
                }}
                wheel
                increment={0.1}
                maxValue={1}
            />
            <NumberField label={{ text: "" }} style={{ width: 100 }} format="n;0;2" value-bind="$page.device.light.brightness" autoFocus />
        </LabeledContainer>
    </cx>
);
