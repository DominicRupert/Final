import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getAccountKeeps() {
    try {
      const res = await api.get('/account/keeps')
      AppState.accountKeeps = res.data
    } catch (err) {
      logger.error('no', err)
    }
  }

  async getAccountVaults() {
    try {
      const res = await api.get('/account/vaults')
      AppState.accountVaults = res.data
    } catch (err) {
      logger.error('no', err)
    }
  }
}

export const accountService = new AccountService()
