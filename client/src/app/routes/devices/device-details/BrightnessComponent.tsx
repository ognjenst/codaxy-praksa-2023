import { LabeledContainer, NumberField, Slider } from "cx/widgets";

export const BrightnessComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Brightness",
                className: "text-xl",
            }}
        >
            <Slider
                value-bind="$page.device.light.brightness"
                valueTooltip={{
                    text: { tpl: "{$page.device.light.brightness:n;2}" },
                    placement: "up",
                }}
                wheel
                increment={2.5}
                maxValue={1}
            />
            <NumberField label={{ text: "" }} format="n;0;2" value-bind="$page.device.light.brightness" autoFocus />
        </LabeledContainer>
    </cx>
);
