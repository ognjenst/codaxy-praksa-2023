import { Repeater, bind, computable } from "cx/ui";
import { Button, LabeledContainer } from "cx/widgets";

export const ColorComponent = () => (
    <cx>
        <LabeledContainer
            label={{
                text: "Color",
                className: "text-xl",
            }}
        >
            <Repeater records={computable("$page.colors", (colors) => Object.keys(colors))}>
                <Button style={{ backgroundColor: computable("$record", (record) => record) }} />
            </Repeater>
        </LabeledContainer>
    </cx>
);
