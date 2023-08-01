import Controller from "./Controller";
import { Button, Section, TextField, Link } from "cx/widgets";
import BackgroundImage from "../../../assets/img/iotbackground.jpg";

export default () => (
    <cx>
        <div
            className="flex flex-col bg-gray-100 h-screen items-center justify-center"
            style={{
                backgroundImage: `url(${BackgroundImage})`,
                backgroundSize: "110%",
                backgroundPosition: "center",
                backgroundRepeat: "no-repeat",
            }}
            controller={Controller}
        >
            <Section className="bg-gray-200 rounded-2xl p-4" style={{ width: "400px", height: "410px", backgroundColor: "#edeffc" }}>
                <div className="flex flex-col items-center space-y-6">
                    <div className="text-center font-sans text-gray-500 text-2xl">WELCOME</div>

                    <TextField
                        className="h-11"
                        style={{ width: "250px", marginTop: "50px" }}
                        placeholder="Username"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.username"
                        icon="user-circle"
                        required
                    />
                    <TextField
                        className="h-11"
                        style={{ width: "250px" }}
                        placeholder="Password"
                        inputType="password"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.password"
                        icon="key"
                        required
                    />
                    <Button
                        style={{ width: "250px", marginTop: "50px", backgroundColor: "#737687" }}
                        className="text-xl h-12 text-gray-100"
                        onClick="login"
                    >
                        LOGIN
                    </Button>
                    <Link href="~/registration" className="font-sans text-gray-500 text-l bg-transparent underline hover:text-gray-700">
                        Create new account
                    </Link>
                </div>
            </Section>
        </div>
    </cx>
);
