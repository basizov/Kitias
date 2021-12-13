import {GroupType} from "../../model/Group/GroupModel";
import {InferActionType} from "../index";

export const initialState = {
  groups: [] as GroupType[],
  error: '',
  loadingInitial: false,
  loading: false
};

export const groupReducer = (state = initialState, action: GroupActionType) => {
  switch (action.type) {
    case "SET_GROUPS":
      return {...state, groups: action.payload};
    case 'SET_GROUP_LOADING':
      return {...state, loading: action.payload};
    case 'SET_GROUP_LOADING_INITIAL':
      return {...state, loadingInitial: action.payload};
    case 'SET_GROUP_ERROR':
      return {...state, error: action.payload};
    default:
      return {...state};
  }
};

export type GroupActionType = InferActionType<typeof groupActions>;

export const groupActions = {
  setGroups: (payload: GroupType[]) => ({
    type: 'SET_GROUPS',
    payload
  } as const),
  setError: (payload: string) => ({
    type: 'SET_GROUP_ERROR',
    payload
  } as const),
  setLoading: (payload: boolean) => ({
    type: 'SET_GROUP_LOADING',
    payload
  } as const),
  setLoadingInitial: (payload: boolean) => ({
    type: 'SET_GROUP_LOADING_INITIAL',
    payload
  } as const)
};
