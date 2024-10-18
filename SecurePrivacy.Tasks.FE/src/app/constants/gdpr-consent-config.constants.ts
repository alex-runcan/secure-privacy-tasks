import { NgcCookieConsentConfig } from 'ngx-cookieconsent';

export const COOKIE_CONFIG: NgcCookieConsentConfig = {
  cookie: {
    domain: 'localhost',
  },
  position: 'bottom-right',
  theme: 'classic',
  palette: {
    popup: {
      background: '#000000',
      text: '#ffffff',
    },
    button: {
      background: '#f1d600',
      text: '#000000',
    },
  },
  type: 'info',
  content: {
    message:
      "This app stores encrypted data about the products, whilst it doesn't store any PII data!",
    href: 'https://gdpr.eu/what-is-gdpr/',
    dismiss: 'Got it!',
    policy: 'GDPR Policy',
  },
};
