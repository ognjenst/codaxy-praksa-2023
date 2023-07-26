import { CategoryAxis, Chart, Gridlines, Legend, LineGraph, NumericAxis, TimeAxis } from "cx/charts";
import { Svg } from "cx/svg";

export const TemperatureGraph = () => (
    <cx>
        <Svg style="width:30vw; height:15vw;">
            <Chart
                offset="20 -30 -30 40"
                axes={{
                    x: { type: TimeAxis, labelRotation: 0, format: "datetime;HHmmN" },
                    y: { type: NumericAxis, vertical: true, min: 0, max: 40 },
                }}
            >
                <Gridlines />
                <LineGraph name="Temperature" data-bind="$page.temperatureChart" colorIndex={14} />
            </Chart>
        </Svg>
    </cx>
);
