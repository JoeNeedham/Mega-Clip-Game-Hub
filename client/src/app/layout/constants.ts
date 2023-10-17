import { NavLink } from './types';

export const sideNavAuthenticatedLinks: NavLink[] = [
  {
    href: '#',
    label: 'Dashboard',
    imageUrl: '../../assets/dashboard-svg.svg',
  },
  {
    href: '#',
    label: 'Profile',
    imageUrl: '../../assets/profile-round-svg.svg',
  },
  {
    href: '#',
    label: 'Games',
    imageUrl: '../../assets/games-svg.svg',
  },
  {
    href: '#',
    label: 'Chat',
    imageUrl: '../../assets/chat-round-svg.svg',
  },
];

export const sideNavUnAuthenticatedLinks: NavLink[] = [
  {
    href: '#',
    label: 'Sign In',
    imageUrl: '../../assets/profile-round-svg.svg',
  },
  {
    href: '#',
    label: 'Sign Up',
    imageUrl: '../../assets/profile-round-svg.svg',
  },
];
