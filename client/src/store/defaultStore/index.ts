import {InferActionType} from "../index";

export enum ColorEnums {
  DARK_COLOR = 'dark',
  LIGHT_COLOR = 'light'
};

const initialState = {
  colorTheme: ColorEnums.DARK_COLOR,
  isAuth: false,
  error: '',
  success: '',
  loadingInitial: true,
  roles: [] as string[],
  loading: false
};

export const defaultReducer = (state = initialState, action: DefaultActionType) => {
  switch (action.type) {
    case 'SET_LIGHT_THEME':
      return {...state, colorTheme: action.payload};
    case 'SET_ROLES':
      return {...state, roles: action.payload};
    case 'SET_DARK_THEME':
      return {...state, colorTheme: action.payload};
    case 'SET_IS_AUTH':
      return {...state, isAuth: action.payload};
    case 'SET_AUTH_LOADING':
      return {...state, loading: action.payload};
    case 'SET_AUTH_LOADING_INITIAL':
      return {...state, loadingInitial: action.payload};
    case 'SET_AUTH_ERROR':
      return {...state, error: action.payload};
    case 'SET_AUTH_SUCCESS':
      return {...state, success: action.payload};
    default:
      return {...state};
  }
};

export type DefaultActionType = InferActionType<typeof defaultActions>;

export const defaultActions = {
  setLightTheme: () => ({
    type: 'SET_LIGHT_THEME',
    payload: ColorEnums.LIGHT_COLOR
  } as const),
  setDarkTheme: () => ({
    type: 'SET_DARK_THEME',
    payload: ColorEnums.DARK_COLOR
  } as const),
  setIsAuth: (payload: boolean) => ({type: 'SET_IS_AUTH', payload} as const),
  setRoles: (payload: string[]) => ({type: 'SET_ROLES', payload} as const),
  setLoading: (payload: boolean) => ({
    type: 'SET_AUTH_LOADING',
    payload
  } as const),
  setLoadingInitial: (payload: boolean) => ({
    type: 'SET_AUTH_LOADING_INITIAL',
    payload
  } as const),
  setError: (payload: string) => ({type: 'SET_AUTH_ERROR', payload} as const),
  setSuccess: (payload: string) => ({type: 'SET_AUTH_SUCCESS', payload} as const)
};
