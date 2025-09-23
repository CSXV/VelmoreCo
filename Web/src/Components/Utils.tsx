import { redirect } from "react-router-dom";

export async function requireAuth(request: any) {
  const pathname = new URL(request.url).pathname;

  const isLoggedIn = localStorage.getItem("isLoggedIn");
  const userID = +localStorage.getItem("userID")!;

  if (isLoggedIn !== "true" || userID <= 0) {
    const response = redirect(
      `/login?message=You must log in first&redirectTo=${pathname}`,
    );

    throw response;
  }

  return null;
}
