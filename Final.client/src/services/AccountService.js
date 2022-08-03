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
 

  async getAccountKeeps(keepId) {
    try {
      const res = await api.get('account/keeps', keepId)
      AppState.accountKeeps = res.data
    } catch (err) {
      logger.error('no', err)
    }
  }

  async getAccountVaults(vaultId) {
    try {
      const res = await api.get('account/vaults', vaultId)
      logger.log('[getAccountVaults]', res.data)
      AppState.accountVaults = res.data
    } catch (err) {
      logger.error('no', err)
    }
  }
}

export const accountService = new AccountService()
