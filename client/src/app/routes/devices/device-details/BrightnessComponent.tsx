import { Slider } from "cx/widgets";

export const BrightnessComponent = ({}) => (
    <cx>
        <div>
            <Slider
                label="Standard"
                value-bind="$page.device.light.brightness"
                valueTooltip={{
                    text: { tpl: "{$page.device.light.brightness:n;2}" },
                    placement: "up",
                }}
                wheel
                increment={2.5}
                maxValue={1}
            />
        </div>
    </cx>
);
