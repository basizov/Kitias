import {InferActionType} from "../index";

export enum ColorEnums {
  DARK_COLOR = 'dark',
  LIGHT_COLOR = 'light'
};

const initialState = {
  colorTheme: ColorEnums.DARK_COLOR,
  isAuth: false
};

export const defaultReducer = (state = initialState, action: DefaultActionType) => {
  switch (action.type) {
    case 'SET_LIGHT_THEME':
      return {...state, colorTheme: action.payload};
    case 'SET_DARK_THEME':
      return {...state, colorTheme: action.payload};
    case 'SET_IS_AUTH':
      return {...state, isAuth: action.payload};
    default:
      return {...state};
  }
};

export type DefaultActionType = InferActionType<typeof defaultActions>;

export const defaultActions = {
  setLightTheme: () => ({
    type: 'SET_LIGHT_THEME',
    payload: ColorEnums.LIGHT_COLOR
  }),
  setDarkTheme: () => ({
    type: 'SET_DARK_THEME',
    payload: ColorEnums.DARK_COLOR
  }),
  setIsAuth: (payload: boolean) => ({type: 'SET_IS_AUTH', payload})
};
