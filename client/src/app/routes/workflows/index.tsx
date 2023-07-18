import { List } from "cx/widgets";
import Controller from './Controller';
import WorkflowsList from "./workflows-list";

export default () => (
    <cx>
        <div controller={Controller} className="m-4 flex flex-col">
            <div class="grid grid-cols-5 gap-3">
                <div class="bg-blue-100">
                   <WorkflowsList />
                </div>
                <div class="bg-red-100 border-2 border-indigo-600 col-span-4 static">
                    <div className="static">
                        <p className="relative -top-3 left-10">Heading</p>
                    </div>
                </div>
            </div>
        </div>       
    </cx>
);