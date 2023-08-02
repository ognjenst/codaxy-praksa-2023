import Controller from "./Controller";
import { Button, Section, TextField, Link } from "cx/widgets";
import BackgroundImage from "../../../assets/img/background2.jpg";

export default () => (
    <cx>
        <div
            className="flex flex-col bg-gray-100 h-screen items-center justify-center"
            controller={Controller}
            style={{
                backgroundImage: `url(${BackgroundImage})`,
                backgroundSize: "180%",
                backgroundPosition: "center",
                backgroundRepeat: "no-repeat",
            }}
        >
            <Section className="bg-gray-200 rounded-2xl p-4" style={{ width: "400px", backgroundColor: "#dededf" }}>
                <div className="flex flex-col items-center space-y-6">
                    <div className="text-center font-sans text-gray-500 text-2xl mb-5">CREATE NEW ACCOUNT</div>

                    <TextField
                        className="h-11"
                        style={{ width: "280px" }}
                        placeholder="First name"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.firstName"
                        required
                    />
                    <TextField
                        className="h-11"
                        style={{ width: "280px" }}
                        placeholder="Last name"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.lastName"
                        required
                    />
                    <TextField
                        className="h-11"
                        style={{ width: "280px" }}
                        placeholder="Email"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.email"
                        icon="envelope"
                        required
                    />
                    <TextField
                        className="h-11"
                        style={{ width: "280px" }}
                        placeholder="Username"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.username"
                        icon="user-circle"
                        required
                    />
                    <TextField
                        className="h-11"
                        style={{ width: "280px" }}
                        placeholder="Password"
                        inputType="password"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.password"
                        icon="key"
                        required
                        onKeyDown="handleEnter"
                    />
                    <Button
                        style={{ width: "280px", marginTop: "50px", backgroundColor: "#4cbdf9" }}
                        className="text-xl h-12 text-gray-100"
                        onClick="registration"
                    >
                        SIGN UP
                    </Button>
                    <Link
                        onClick={(e, { store }) => {
                            store.set("login", true);
                        }}
                        className="font-sans text-gray-500 text-l bg-transparent underline hover:text-gray-700"
                    >
                        Already have an account? Sign in here.
                    </Link>
                </div>
            </Section>
        </div>
    </cx>
);
