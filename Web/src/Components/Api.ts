import type { Creds, registerCreds, User } from "./Types";

// ----------------------------------------------------------
const apiURL = "http://localhost:5244/api";

// ----------------------------------------------------------
export async function getAllProducts() {
  const res = await fetch(`${apiURL}/Products/All`);
  const data = await res.json();

  if (!res.ok) {
    throw {
      message: data.message,
      statusText: res.statusText,
      status: res.status,
    };
  }

  return data;
}

export async function getAllProductReviews(id: number) {
  const res = await fetch(`${apiURL}/Reviews/AllProductReviews/${id}`);
  const data = await res.json();

  if (!res.ok) {
    throw {
      message: data.message,
      statusText: res.statusText,
      status: res.status,
    };
  }

  return data;
}

// ----------------------------------------------------------
export async function registerNewUser(creds: registerCreds) {
  const response = await fetch(`${apiURL}/Users/Add`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(creds),
  });

  const data = await response.json();

  if (!response.ok) {
    throw {
      message: data.message,
      statusText: response.statusText,
      status: response.status,
    };
  }

  return data;
}

export async function updateUserByID(updatedUser: User) {
  const response = await fetch(`${apiURL}/Users/Update/${updatedUser.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(updatedUser),
  });

  const data = await response.json();

  if (!response.ok) {
    throw {
      message: data.message,
      statusText: response.statusText,
      status: response.status,
    };
  }

  return data;
}

export async function deleteUserByID(id: number) {
  const response = await fetch(`${apiURL}/Users/Delete/${id}`, {
    method: "Delete",
  });

  const data = await response.json();

  if (!response.ok) {
    throw {
      message: data.message,
      statusText: response.statusText,
      status: response.status,
    };
  }

  return data;
}

export async function getUserByID(id: number) {
  const response = await fetch(`${apiURL}/Users/${id}`);

  const data = await response.json();

  if (!response.ok) {
    throw {
      message: data.message,
      statusText: response.statusText,
      status: response.status,
    };
  }

  return data;
}

export async function loginUser(creds: Creds) {
  const response = await fetch(`${apiURL}/Users/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(creds),
  });

  const data = await response.json();

  if (!response.ok) {
    throw {
      message: data.message,
      statusText: response.statusText,
      status: response.status,
    };
  }

  return data;
}
