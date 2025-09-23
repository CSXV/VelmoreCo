export type product = {
  id: number;
  name: string;
  description: string;
  categoryID: number;
  price: number;
  material: string;
  gemstone: string;
  carat: number;
  clarity: string;
  cut: string;
  certification: string;
  stock: number;
  isFeatured: boolean;
  createDate: string;
  updateDat: string;
};

export interface CartProduct extends product {
  quantity: number;
}

export type User = {
  id: number;
  email: string;
  password: string;

  firstName: string;
  lastName: string;

  createDate: string;
  updateDate: string;

  userType: number;
};

export interface Creds {
  email: string;
  password: string;
}

export interface registerCreds {
  firstName: string;
  lastName: string;
  password: string;
  email: string;
}

export interface Reviews {
  id: number;
  productID: number;
  userID: number;
  rating: number;
  comment: string;
  createDate: string;

  firstName: string;
  lastName: string;
}
