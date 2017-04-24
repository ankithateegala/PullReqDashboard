import { PullReqDashboard.ApplicationPage } from './app.po';

describe('pull-req-dashboard.application App', () => {
  let page: PullReqDashboard.ApplicationPage;

  beforeEach(() => {
    page = new PullReqDashboard.ApplicationPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
