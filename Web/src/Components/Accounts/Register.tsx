import { Form, redirect, useActionData, useNavigation } from "react-router-dom";
import { registerNewUser } from "../Api";
import type { registerCreds } from "../Types";

export async function action(obj: any) {
  const formData = await obj.request.formData();

  if (formData.get("password") !== formData.get("confirmPassword")) return;

  const url =
    new URL(obj.request.url).searchParams.get("redirectTo") || "/Shop";

  const cred: registerCreds = {
    email: formData.get("email"),
    password: formData.get("password"),
    firstName: formData.get("firstName"),
    lastName: formData.get("lastName"),
  };

  try {
    const data = await registerNewUser(cred);

    localStorage.setItem("isLoggedIn", "true");
    localStorage.setItem("userID", data.id);

    return redirect(url);
  } catch (error) {
    // return (error as any).message;

    if (error instanceof Error) {
      return error.message;
    }
  }
}

export default function Register() {
  const navigate = useNavigation();
  const errorMessage = useActionData();

  return (
    <div className="login-container">
      <h2>Wellcome new user</h2>

      <div>
        {errorMessage && <h3 className="button Cancel">{errorMessage}</h3>}
      </div>

      {navigate.state === "submitting" ? (
        <h2>Loading...</h2>
      ) : (
        <Form className="card-login" method="post" replace>
          <div className="input-container">
            <label htmlFor="email">
              Email:
              <br />
            </label>
            <input
              id="email"
              className="button input"
              name="email"
              type="email"
              placeholder="JanDoe@example.com"
              autoComplete="email"
            />

            <br />
            <label htmlFor="firstName">
              First name:
              <br />
            </label>
            <input
              id="firstName"
              className="button input"
              name="firstName"
              type="text"
              placeholder="Jan"
              autoComplete="name"
            />

            <br />
            <label htmlFor="lastName">
              Last name:
              <br />
            </label>
            <input
              id="lastName"
              className="button input"
              name="lastName"
              type="text"
              placeholder="Doe"
              autoComplete="name"
            />

            <br />
            <label htmlFor="password">
              Password:
              <br />
            </label>
            <input
              id="password"
              className="button input"
              name="password"
              type="password"
              autoComplete="current-password"
            />

            <br />
            <label htmlFor="confirmPassword">
              Confirm password:
              <br />
            </label>
            <input
              id="confirmPassword"
              className="button input"
              name="confirmPassword"
              type="password"
            />
          </div>

          <br />
          <button type="submit" className="button glass black-text">
            Register
          </button>
        </Form>
      )}
    </div>
  );
}
