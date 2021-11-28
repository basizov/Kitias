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
  EventAvailable,
  Home,
  DateRange, FeaturedPlayList
} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";

const openMixin = (theme: Theme): CSSObject => ({
  width: 300,
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.enteringScreen
  }),
  overflowX: 'hidden'
});

const closeMixin = (theme: Theme): CSSObject => ({
  transition: theme.transitions.create('width', {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen
  }),
  overflowX: 'hidden',
  width: `calc(${theme.spacing(5)} + 1px)`,
  [theme.breakpoints.up('sm')]: {
    width: `calc(${theme.spacing(7)} + 1px)`
  }
});

const DrawerHeader = styled('div')(({theme}) => ({
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  ...theme.mixins.toolbar
}));

const StyledLeftArrow = styled(DrawerHeader)({
  justifyContent: 'flex-end',
  marginRight: '1rem'
});

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
  })
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
      <List>
        <ListItem disablePadding>
          <ListItemButton onClick={() => navigate('/')}>
            <ListItemIcon>
              <Home/>
            </ListItemIcon>
            <ListItemText primary="Главная"/>
          </ListItemButton>
        </ListItem>
        <ListItem disablePadding>
          <ListItemButton onClick={() => navigate('/subjects')}>
            <ListItemIcon>
              <FeaturedPlayList/>
            </ListItemIcon>
            <ListItemText primary="Предметы"/>
          </ListItemButton>
        </ListItem>
        <ListItem disablePadding>
          <ListItemButton onClick={() => navigate('/calendar')}>
            <ListItemIcon>
              <EventAvailable/>
            </ListItemIcon>
            <ListItemText primary="Календарь"/>
          </ListItemButton>
        </ListItem>
        <ListItem disablePadding>
          <ListItemButton onClick={() => navigate('/attendances')}>
            <ListItemIcon>
              <DateRange/>
            </ListItemIcon>
            <ListItemText primary="График посещений"/>
          </ListItemButton>
        </ListItem>
      </List>
    </StyledDrawer>
  );
};
