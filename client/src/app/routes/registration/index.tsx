import Controller from "./Controller";
import { Button, Section, TextField, Link } from "cx/widgets";

export default () => (
    <cx>
        <div className="flex flex-col bg-gray-100 h-screen items-center justify-center" controller={Controller}>
            <Section className="bg-gray-200 rounded-2xl p-4" style={{ width: "400px" }}>
                <div className="flex flex-col items-center space-y-6">
                    <div className="text-center font-sans text-gray-500 text-2xl mb-5">CREATE NEW ACCOUNT</div>

                    <TextField
                        className="h-11"
                        style={{ width: "280px" }}
                        placeholder="First name"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.firstname"
                        required
                    />
                    <TextField
                        className="h-11"
                        style={{ width: "280px" }}
                        placeholder="Last name"
                        inputStyle={{ fontSize: "16px" }}
                        value-bind="$page.user.lastname"
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
                    />
                    <Button
                        style={{ width: "280px", marginTop: "50px" }}
                        className="text-xl h-12 bg-gray-100 text-gray-600"
                        onClick="registration"
                    >
                        SIGN UP
                    </Button>
                </div>
            </Section>
        </div>
    </cx>
);
