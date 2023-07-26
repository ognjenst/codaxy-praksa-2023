import { CategoryAxis, Chart, Gridlines, LineGraph, NumericAxis, TimeAxis } from "cx/charts";
import { Svg } from "cx/svg";

export const PowerGraph = () => (
    <cx>
        <Svg style="width:30vw; height:15vw;">
            <Chart
                offset="20 -10 -60 40"
                axes={{
                    x: { type: TimeAxis, labelRotation: 0, format: "datetime;HHmmN" },
                    y: { type: NumericAxis, vertical: true },
                }}
            >
                <Gridlines />
                <LineGraph name="Power" data-bind="$page.powerChart" colorIndex={7} />
            </Chart>
        </Svg>
    </cx>
);
