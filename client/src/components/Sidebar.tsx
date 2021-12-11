import React from 'react';
import {
  CSSObject,
  Divider,
  Drawer,
  IconButton,
  List,
  ListItem, ListItemButton, ListItemIcon, ListItemText,
  styled, Theme
} from "@mui/material";
import {
  ChevronLeft,
  Menu,
  Home,
  DateRange, FeaturedPlayList, Logout
} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";
import {useDispatch} from "react-redux";
import {logoutAsync} from "../store/defaultStore/asyncActions";
import {useTypedSelector} from "../hooks/useTypedSelector";

const openMixin = (theme: Theme): CSSObject => ({
  width: 300,
  transition: theme.transitions.create(['width', 'height'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.enteringScreen
  }),
  overflowX: 'hidden',
  [theme.breakpoints.down('sm')]: {
    width: '100%'
  }
});

const closeMixin = (theme: Theme): CSSObject => ({
  transition: theme.transitions.create(['width', 'height'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen
  }),
  overflowX: 'hidden',
  width: `calc(${theme.spacing(5)} + 1px)`,
  [theme.breakpoints.up('sm')]: {
    width: `calc(${theme.spacing(7)} + 1px)`
  },
  [theme.breakpoints.down('sm')]: {
    width: '100%',
    height: `calc(${theme.spacing(7)} + 1px)`,
    overflow: 'hidden'
  }
});

const DrawerHeader = styled('div')(({theme}) => ({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  ...theme.mixins.toolbar,
  [theme.breakpoints.down('sm')]: {
    justifyContent: 'flex-start',
    marginLeft: '1rem',
    minHeight: `${theme.spacing(7)} !important`
  },
  [theme.breakpoints.down('xs')]: {
    minHeight: '0 !important'
  }
}));

const StyledLeftArrow = styled(DrawerHeader)(({theme}) => ({
  justifyContent: 'flex-end',
  marginRight: '1rem'
}));

const StyledDrawer = styled(Drawer, {
  shouldForwardProp: (prop) => prop !== 'open'
})(({theme, open}) => ({
  width: 300,
  flexShrink: 0,
  whiteSpace: 'nowrap',
  boxSizing: 'border-box',
  ...(open && {
    ...openMixin(theme),
    '& .MuiDrawer-paper': openMixin(theme)
  }),
  ...(!open && {
    ...closeMixin(theme),
    '& .MuiDrawer-paper': closeMixin(theme)
  }),
  [theme.breakpoints.down('sm')]: {
    position: 'absolute',
    top: 0,
    left: 0,
    width: '100%',
    flexDirection: 'row'
  }
}));

const StyledListItem = styled(ListItem)(({theme}) => ({
  [theme.breakpoints.down('sm')]: {
    display: 'none'
  }
}));

const LogoutItem = styled(ListItem)(({theme}) => ({
  position: 'absolute',
  bottom: '.3rem'
}));

type PropsType = {
  open: boolean;
  setOpen: (payload: boolean) => void;
};

export const Sidebar: React.FC<PropsType> = ({
                                               open,
                                               setOpen
                                             }) => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const {isAuth} = useTypedSelector(s => s.common);

  return (
    <StyledDrawer variant="permanent" open={open}>
      {!open ?
        <DrawerHeader>
          <IconButton
            onClick={() => setOpen(true)}
          ><Menu/></IconButton>
        </DrawerHeader> :
        <StyledLeftArrow>
          <IconButton
            onClick={() => setOpen(false)}
          ><ChevronLeft/></IconButton>
        </StyledLeftArrow>}
      <Divider/>
      <List sx={{height: '100%'}}>
        <StyledListItem disablePadding>
          <ListItemButton onClick={() => {
            navigate('/');
            setOpen(false);
          }}>
            <ListItemIcon>
              <Home/>
            </ListItemIcon>
            <ListItemText primary="Главная"/>
          </ListItemButton>
        </StyledListItem>
        <ListItem disablePadding>
          <ListItemButton onClick={() => {
            navigate('/subjects');
            setOpen(false);
          }}>
            <ListItemIcon>
              <FeaturedPlayList/>
            </ListItemIcon>
            <ListItemText primary="Предметы"/>
          </ListItemButton>
        </ListItem>
        <ListItem disablePadding>
          <ListItemButton onClick={() => {
            navigate('/attendances');
            setOpen(false);
          }}>
            <ListItemIcon>
              <DateRange/>
            </ListItemIcon>
            <ListItemText primary="Журналы посещений"/>
          </ListItemButton>
        </ListItem>
        {isAuth && <LogoutItem disablePadding>
            <ListItemButton onClick={async () => {
              await dispatch(logoutAsync());
              setOpen(false);
            }}>
                <ListItemIcon>
                    <Logout/>
                </ListItemIcon>
                <ListItemText primary="Выйти"/>
            </ListItemButton>
        </LogoutItem>}
      </List>
    </StyledDrawer>
  );
};
