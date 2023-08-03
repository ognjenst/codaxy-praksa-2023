import { List, Window } from "cx/widgets";

export const openShowInputParameters = ({ props }) => {
    let w = Window.create(
        <cx>
            <Window title="Workflow Input Parameters" center className="w-[40vw] h-[40vw]" bodyClass="p-4" modal draggable closeOnEscape>
                <List records={props.arr}>
                    <div text-bind="$record" />
                </List>
            </Window>
        </cx>
    );

    w.open();
};
