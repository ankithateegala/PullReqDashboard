import { PullReqDashboard.AppPage } from './app.po';

describe('pull-req-dashboard.app App', () => {
  let page: PullReqDashboard.AppPage;

  beforeEach(() => {
    page = new PullReqDashboard.AppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
