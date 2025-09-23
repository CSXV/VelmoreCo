import {
  Form,
  Link,
  redirect,
  useActionData,
  useLoaderData,
  useNavigation,
} from "react-router-dom";
import { loginUser } from "../Api";

export async function action(obj: any) {
  const formData = await obj.request.formData();

  const url =
    new URL(obj.request.url).searchParams.get("redirectTo") || "/Shop";

  const cred = {
    email: formData.get("email"),
    password: formData.get("password"),
  };

  try {
    const data = await loginUser(cred);

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

export function loader({ request }: any) {
  return new URL(request.url).searchParams.get("message");
}

function Login() {
  const message = useLoaderData();
  const errorMessage = useActionData();
  const navigate = useNavigation();

  return (
    <div className="login-container">
      <h1>sign in to your account</h1>

      <div>
        {message && <h4 className="Cancel">{message}!</h4>}

        {errorMessage && <h4 className="Cancel black-text">{errorMessage}</h4>}
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
            <label htmlFor="password">
              Password:
              <br />
            </label>
            <input
              className="button input"
              name="password"
              type="password"
              placeholder="Password"
              autoComplete="current-password"
            />
          </div>

          <br />
          <button type="submit" className="button glass black-text">
            Login
          </button>
        </Form>
      )}

      <p>
        Don't have an account? <Link to="../register">Register now</Link>
      </p>
    </div>
  );
}

export default Login;
