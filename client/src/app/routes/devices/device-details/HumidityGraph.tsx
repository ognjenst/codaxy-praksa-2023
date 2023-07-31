import { CategoryAxis, Chart, Gridlines, LineGraph, NumericAxis, TimeAxis } from "cx/charts";
import { Svg } from "cx/svg";

export const HumidityGraph = () => (
    <cx>
        <Svg style="width:30vw; height:15vw;">
            <Chart
                offset="20 -30 -30 40"
                axes={{
                    x: { type: TimeAxis, labelRotation: 0, format: "datetime;HHmmN" },
                    y: { type: NumericAxis, vertical: true, min: 20, max: 100 },
                }}
            >
                <Gridlines />
                <LineGraph name="Humidity" data-bind="$page.humidityChart" colorIndex={6} />
            </Chart>
        </Svg>
    </cx>
);
