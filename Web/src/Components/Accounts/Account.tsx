import {
  Await,
  Form,
  redirect,
  useActionData,
  useLoaderData,
  useNavigation,
} from "react-router-dom";
import { requireAuth } from "../Utils";
import { deleteUserByID, getUserByID, loginUser, updateUserByID } from "../Api";
import type { User } from "../Types";
import { Suspense } from "react";

export async function loader({ params, request }: any) {
  await requireAuth(request);

  return { user: getUserByID(params.id) };
}

export async function action(obj: any) {
  const formData = await obj.request.formData();
  if (formData.get("password") !== formData.get("confirmPassword")) return;

  const url =
    new URL(obj.request.url).searchParams.get("redirectTo") || "/Shop";

  const date = new Date();

  const updatedUser: User = {
    id: +localStorage.getItem("userID")!,

    firstName: formData.get("firstName"),
    lastName: formData.get("lastName"),

    email: formData.get("email"),
    password: formData.get("password"),

    createDate: date.toISOString(),
    updateDate: date.toISOString(),

    userType: 0,
  };

  const cred = {
    email: formData.get("OriginalEmail"),
    password: formData.get("password"),
  };

  const login = await loginUser(cred);
  if (login === null) return;

  try {
    const data = await updateUserByID(updatedUser);
    if (data === null) return;

    return redirect(url);
  } catch (error: any) {
    return error.message;
  }
}

function Account() {
  const userData = useLoaderData();
  const navigate = useNavigation();
  const errorMessage = useActionData();

  function handleAccountDeletion() {
    deleteUserByID(+localStorage.getItem("userID")!);

    localStorage.setItem("userID", "0");
    localStorage.setItem("isLoggedIn", "false");

    return redirect("/");
  }

  function renderUserEditElements(user: User) {
    return (
      <div className="login-container">
        <h2>Update your info</h2>

        <div>
          {errorMessage && <h3 className="black-text">{errorMessage}</h3>}
        </div>

        {navigate.state === "submitting" ? (
          <h2>Loading...</h2>
        ) : (
          <Form className="card-login" method="post" replace>
            <div className="input-container">
              <input
                id="OriginalEmail"
                name="OriginalEmail"
                className="button input"
                type="email"
                defaultValue={user.email}
                hidden
              />

              <label htmlFor="email">
                Email:
                <br />
              </label>
              <input
                id="email"
                className="button input"
                name="email"
                type="email"
                placeholder="Email"
                autoComplete="email"
                defaultValue={user.email}
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
                placeholder="First name"
                autoComplete="name"
                defaultValue={user.firstName}
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
                placeholder="Last name"
                autoComplete="name"
                defaultValue={user.lastName}
              />

              <label htmlFor="password">
                Password:
                <br />
              </label>
              <input
                id="password"
                className="button input"
                name="password"
                type="password"
                // placeholder="Password"
                autoComplete="current-password"
              />

              <label htmlFor="confirmPassword">
                Confirm password:
                <br />
              </label>
              <input
                id="confirmPassword"
                className="button input"
                name="confirmPassword"
                type="password"
                // placeholder="Confirm password"
              />
            </div>

            <br />
            <button type="submit" className="button glass black-text">
              Update
            </button>
          </Form>
        )}

        <div>
          <button
            onClick={handleAccountDeletion}
            type="button"
            className="button glass black-text"
          >
            Delete account
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="notes-container">
      <Suspense>
        <Await resolve={userData.user}>{renderUserEditElements}</Await>
      </Suspense>
    </div>
  );
}

export default Account;
